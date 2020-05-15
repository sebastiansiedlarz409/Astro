package com.example.astromobile

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.view.View
import androidx.appcompat.app.AppCompatActivity
import com.example.astromobile.services.AuthService
import com.example.astromobile.services.RegisterResults
import kotlinx.android.synthetic.main.activity_register.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.launch

class RegisterActivity : AppCompatActivity() {

    private lateinit var authService: AuthService
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register)
        supportActionBar?.hide()

        sharedPreferences = getSharedPreferences("AstroMobile", Context.MODE_PRIVATE)
        authService = AuthService.getAuthService(sharedPreferences)!!
    }

    fun register(view: View){
        val username = username.text.toString()
        val email = email.text.toString()
        val password = password.text.toString()
        val passwordConfirm = passwordConfirm.text.toString()

        CoroutineScope(IO).launch {
            val registerResult: RegisterResults = authService.register(username, email, password, passwordConfirm)

            when (registerResult) {
                RegisterResults.Registered -> {
                    val intent = Intent(this@RegisterActivity, LoginActivity::class.java)
                    intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
                    startActivity(intent)
                }
                RegisterResults.BadRequest -> {
                    //TODO: print error
                }
                else -> {
                    //TODO: print error
                }
            }
        }
    }
}
