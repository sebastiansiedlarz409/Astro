package com.example.astromobile.services

import android.content.Context
import android.content.SharedPreferences
import com.example.astromobile.apiclient.ApiClientAuth
import com.example.astromobile.apiclient.ApiClientForum
import com.example.astromobile.models.Token
import com.example.astromobile.models.User
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.async
import okhttp3.Response

class AuthService(context: Context){

    private lateinit var apiClient: ApiClientAuth
    private val sharedPreferences = context.getSharedPreferences("ASTRO", Context.MODE_PRIVATE);
    private var token: Token? = null
    private var loginTime: Long = 0

    init {
        apiClient = ApiClientAuth(sharedPreferences)
    }

    fun isLogged(): Boolean{
        return System.currentTimeMillis() - loginTime < 600000 && token != null
    }

    fun getLoggedUserToken(): Token?{
        return token
    }

    fun getLoggedUser(): User?{
        return token?.user
    }

    fun getLoggedUserRole(): String?{
        return token?.roles?.get(0)
    }

    suspend fun login(email: String, password: String): LoginResults{

        var result: LoginResults = LoginResults.Other

        CoroutineScope(IO).async {
            val response: Response = apiClient.login(email, password)

            when (response.code) {
                200 -> {
                    token = apiClient.loginData(response.body?.string())
                    result = LoginResults.Logged

                    loginTime = System.currentTimeMillis()
                }
                400 -> {
                    result = LoginResults.BadRequest
                }
                else -> {
                    result = LoginResults.Other
                }
            }
        }.await()

        return result
    }

    suspend fun register(username: String, email: String, password: String, passwordConfirm: String): RegisterResults{
        var result: RegisterResults = RegisterResults.Other

        CoroutineScope(IO).async {
            val response: Response = apiClient.register(username, email, password, passwordConfirm)

            result = when (response.code) {
                200 -> {
                    RegisterResults.Registered
                }
                400 -> {
                    RegisterResults.BadRequest
                }
                else -> {
                    RegisterResults.Other
                }
            }
        }.await()

        return result
    }

    fun logOut(){
        token = null
    }

    companion object{
        private var instance: AuthService? = null

        fun getAuthService(context: Context): AuthService?{
            if(instance == null){
                instance = AuthService(context)
            }

            return instance
        }
    }
}