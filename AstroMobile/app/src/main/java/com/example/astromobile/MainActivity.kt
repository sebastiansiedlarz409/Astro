package com.example.astromobile

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.view.WindowManager.LayoutParams.FLAG_DRAWS_SYSTEM_BAR_BACKGROUNDS
import androidx.activity.OnBackPressedCallback
import androidx.appcompat.app.AlertDialog
import com.example.astromobile.adapters.MenuAdapter
import com.example.astromobile.services.AuthService
import com.google.android.material.snackbar.Snackbar
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.activity_main.login
import java.lang.System.exit
import kotlin.system.exitProcess

class MainActivity : AppCompatActivity() {

    private lateinit var authService: AuthService

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        supportActionBar?.hide()

        authService = AuthService.getAuthService()!!

        val callback = object : OnBackPressedCallback(true) {
            override fun handleOnBackPressed() {
                finish()
            }
        }
        this.onBackPressedDispatcher.addCallback(this, callback)

        login.setOnClickListener {
            startActivity(Intent(this, LoginActivity::class.java))
        }
        register.setOnClickListener {
            startActivity(Intent(this, RegisterActivity::class.java))
        }
        loginOut.setOnClickListener {
            authService.logOut()
            finish()
            startActivity(intent)
        }

        if(authService.isLogged()){
            authField.visibility = View.GONE
            loggedField.visibility = View.VISIBLE

            loggedUser.text = authService.getLoggedUser()?.userName
        }

        val options: ArrayList<String> = arrayListOf("APOD", "EPIC", "Asteroids", "Insight", "Galeria", "Forum", "Informacje")

        val adapter = MenuAdapter(this, options)

        menu.adapter = adapter

        menu.setOnItemClickListener{
                _, _, position, _ ->
            if(position == 0){
                startActivity(Intent(this, APODActivity::class.java))
            }
            else if(position == 1){
                startActivity(Intent(this, EPICActivity::class.java))
            }
            else if(position == 2){
                startActivity(Intent(this, AsteroidsNeoWsActivity::class.java))
            }
            else if(position == 3){
                startActivity(Intent(this, InsightActivity::class.java))
            }
            else if(position == 4){

            }
            else if(position == 5){
                if(!authService.isLogged()){
                    val builder = AlertDialog.Builder(this, R.style.InfoAlert)
                    builder.setTitle("Forum")
                    builder.setMessage("Musisz się zalogować!")
                    builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                    builder.setPositiveButton("Rejestruj") { _, _ ->
                        startActivity(Intent(this@MainActivity, RegisterActivity::class.java))
                    }
                    builder.setNegativeButton("Zaloguj") { _, _ ->
                        startActivity(Intent(this@MainActivity, LoginActivity::class.java))
                    }
                    builder.show()
                }
                else{
                    startActivity(Intent(this, ForumActivity::class.java))
                }
            }
            else{
                val builder = AlertDialog.Builder(this, R.style.InfoAlert)
                builder.setTitle("Informacje")
                builder.setMessage("Mobilny klient portalu Astro")
                builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                builder.setPositiveButton("OK") { dialog, _ ->
                    dialog.dismiss()
                }
                builder.setNegativeButton("Github") { _, _ ->
                    startActivity(Intent(Intent.ACTION_VIEW, Uri.parse("https://github.com/sebastiansiedlarz409")))
                }
                builder.show()
            }
        }
    }
}
