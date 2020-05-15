package com.example.astromobile

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import com.example.astromobile.apiclient.ApiClient
import com.example.astromobile.models.Token
import kotlinx.android.synthetic.main.activity_login.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.launch

class LoginActivity : AppCompatActivity() {

    private val apiClient = ApiClient()
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)
        supportActionBar?.hide()

        sharedPreferences = getSharedPreferences("AstroMobile", Context.MODE_PRIVATE)
    }

    fun login(view: View){
        CoroutineScope(IO).launch {
            val data: String? = apiClient.login(email.text.toString(), password.text.toString())
            if(data == null){
                val intent = Intent(this@LoginActivity, LoginActivity::class.java)
                intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
                startActivity(intent)
            }
            else{
                val token: Token = apiClient.loginData(data)
                sharedPreferences.edit().putString("token", token.token).apply()
                sharedPreferences.edit().putString("username", token.user.userName).apply()

                val intent = Intent(this@LoginActivity, MainActivity::class.java)
                intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
                startActivity(intent)
            }
        }
    }
}
