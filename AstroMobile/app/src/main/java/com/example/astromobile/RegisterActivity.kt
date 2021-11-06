package com.example.astromobile

import android.content.Intent
import android.os.Bundle
import android.view.View
import androidx.appcompat.app.AppCompatActivity
import com.example.astromobile.services.AuthService
import com.example.astromobile.services.RegisterResults
import kotlinx.android.synthetic.main.activity_register.*
import kotlinx.android.synthetic.main.activity_register.email
import kotlinx.android.synthetic.main.activity_register.errorText
import kotlinx.android.synthetic.main.activity_register.password
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class RegisterActivity : AppCompatActivity() {

    private lateinit var authService: AuthService

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_register)
        supportActionBar?.hide()

        authService = AuthService.getAuthService(this)!!
    }

    fun register(view: View) {
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
                    withContext(Dispatchers.Main){
                        errorText.visibility = View.VISIBLE
                        errorText.text = "Prawdopodobnie podałeś błędne dane!"
                    }
                }
                else -> {
                    withContext(Dispatchers.Main){
                        errorText.visibility = View.VISIBLE
                        errorText.text = "Prawdopodobnie wystąpił błąd połączenia!"
                    }
                }
            }
        }
    }
}
