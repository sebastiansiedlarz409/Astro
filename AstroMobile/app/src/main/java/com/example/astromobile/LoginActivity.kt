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
import kotlinx.coroutines.Dispatchers.Main
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class LoginActivity : AppCompatActivity() {

    private lateinit var authService: AuthService

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)
        supportActionBar?.hide()

        authService = AuthService.getAuthService(this)!!

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
                    withContext(Main){
                        errorText.visibility = View.VISIBLE
                        errorText.text = "Prawdopodobnie podałeś błędne dane!"
                    }
                }
                else -> {
                    withContext(Main){
                        errorText.visibility = View.VISIBLE
                        errorText.text = "Prawdopodobnie wystąpił błąd połączenia!"
                    }
                }
            }
        }
    }
}
