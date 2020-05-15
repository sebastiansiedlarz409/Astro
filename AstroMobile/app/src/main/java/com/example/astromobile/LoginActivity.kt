package com.example.astromobile

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.activity.OnBackPressedCallback
import com.example.astromobile.services.AuthService
import com.example.astromobile.services.LoginResults
import kotlinx.android.synthetic.main.activity_login.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.launch

class LoginActivity : AppCompatActivity() {

    private lateinit var authService: AuthService
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)
        supportActionBar?.hide()

        sharedPreferences = getSharedPreferences("AstroMobile", Context.MODE_PRIVATE)
        authService = AuthService.getAuthService(sharedPreferences)!!

        val callback = object : OnBackPressedCallback(true) {
            override fun handleOnBackPressed() {
                startActivity(Intent(this@LoginActivity, MainActivity::class.java))
            }
        }
        this.onBackPressedDispatcher.addCallback(this, callback)
    }

    fun login(view: View){
        val email = email.text.toString()
        val password = password.text.toString()

        CoroutineScope(IO).launch {

            val loginResult: LoginResults = authService.login(email, password)

            when (loginResult) {
                LoginResults.Logged -> {
                    val intent = Intent(this@LoginActivity, MainActivity::class.java)
                    intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
                    startActivity(intent)
                }
                LoginResults.BadRequest -> {
                    //TODO: print error
                    /*val intent = Intent(this@LoginActivity, LoginActivity::class.java)
                        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
                        startActivity(intent)*/
                }
                else -> {
                    //TODO: print error
                }
            }
        }
    }
}
