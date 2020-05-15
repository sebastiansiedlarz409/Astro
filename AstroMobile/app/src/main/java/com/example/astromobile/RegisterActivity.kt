package com.example.astromobile

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import com.example.astromobile.apiclient.ApiClient
import com.example.astromobile.models.Token
import kotlinx.android.synthetic.main.activity_register.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.launch

class RegisterActivity : AppCompatActivity() {

    private val apiClient = ApiClient()
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register)
        supportActionBar?.hide()

        sharedPreferences = getSharedPreferences("AstroMobile", Context.MODE_PRIVATE)
    }

    fun register(view: View){
        val username = username.text.toString()
        val email = email.text.toString()
        val password = password.text.toString()
        val passwordConfirm = passwordConfirm.text.toString()

        CoroutineScope(IO).launch {
            var data = apiClient.register(username, email, password, passwordConfirm)
            if(data == null){
                val intent = Intent(this@RegisterActivity, RegisterActivity::class.java)
                intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
                startActivity(intent)
            }
            else{
                data = apiClient.login(email, password)

                if(data == null){
                    val intent = Intent(this@RegisterActivity, LoginActivity::class.java)
                    intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
                    startActivity(intent)
                }
                else{
                    val token: Token = apiClient.loginData(data)
                    sharedPreferences.edit().putString("token", token.token).apply()
                    sharedPreferences.edit().putString("username", token.user.userName).apply()

                    val intent = Intent(this@RegisterActivity, MainActivity::class.java)
                    intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
                    startActivity(intent)
                }
            }
        }
    }
}
