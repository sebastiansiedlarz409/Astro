package com.example.astromobile.apiclient

import android.content.SharedPreferences
import com.example.astromobile.models.Token
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import okhttp3.Request
import okhttp3.RequestBody
import okhttp3.Response
import org.json.JSONObject
import ru.gildor.coroutines.okhttp.await

class ApiClientAuth(sharedPreferences: SharedPreferences): ApiClient(sharedPreferences) {

    private val urlLogin: String = "api/Auth/Login"
    private val urlRegister: String = "api/Auth/Register"

    suspend fun login(email: String, password: String): Response {
        val data = JSONObject()
        data.put("Email", email)
        data.put("Password", password)

        val request = Request.Builder()
            .url("$urlMain/$urlLogin")
            .post(RequestBody.create(JSON, data.toString()))
            .build()

        return client.newCall(request).await()
    }

    fun loginData(data: String?): Token {
        return Gson().fromJson(data, object : TypeToken<Token>() {}.type)
    }

    suspend fun register(username: String, email: String, password: String, passwordConfirm: String): Response {
        val data = JSONObject()
        data.put("UserName", username)
        data.put("Email", email)
        data.put("Password", password)
        data.put("ConfirmPassword", passwordConfirm)

        val request = Request.Builder()
            .url("$urlMain/$urlRegister")
            .post(RequestBody.create(JSON, data.toString()))
            .build()

        return client.newCall(request).await()
    }
}