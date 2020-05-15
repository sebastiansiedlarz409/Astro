package com.example.astromobile

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.activity.OnBackPressedCallback
import com.example.astromobile.apiclient.ApiClient
import kotlinx.android.synthetic.main.activity_connection_check.*
import kotlinx.coroutines.*
import kotlin.system.exitProcess

class ConnectionCheckActivity : AppCompatActivity() {

    private val apiClient = ApiClient()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_connection_check)
        supportActionBar?.hide()

        val callback = object : OnBackPressedCallback(true) {
            override fun handleOnBackPressed() {
                exitProcess(0)
            }
        }
        this.onBackPressedDispatcher.addCallback(this, callback)

        retry.callOnClick()
    }

    fun checkConnection(view: View){
        retry.visibility = View.GONE
        progress.visibility = View.VISIBLE
        info.text = "Sprawdzam połączenie z ASTRO!"

        CoroutineScope(Dispatchers.IO).launch {
            delay(2500)
            if(!apiClient.connectionTest()){
                withContext(Dispatchers.Main){
                    info.text = "Nie można połączyć z ASTRO!"
                    retry.visibility = View.VISIBLE
                    progress.visibility = View.GONE
                }
            }
            else{
                withContext(Dispatchers.Main){
                    startActivity(Intent(this@ConnectionCheckActivity, MainActivity::class.java))
                    finish()
                }
            }
        }
    }
}
