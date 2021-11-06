package com.example.astromobile

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Toast
import androidx.activity.OnBackPressedCallback
import com.example.astromobile.apiclient.ApiClient
import kotlinx.android.synthetic.main.activity_connection_check.*
import kotlinx.coroutines.*
import kotlin.system.exitProcess

class ConnectionCheckActivity : AppCompatActivity() {

    private lateinit var apiClient: ApiClient
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_connection_check)
        supportActionBar?.hide()

        sharedPreferences = getSharedPreferences("ASTRO", Context.MODE_PRIVATE);
        apiClient = ApiClient(sharedPreferences)

        change.visibility = View.GONE
        save.visibility = View.GONE
        address.visibility = View.GONE

        address.setText(sharedPreferences.getString("address", "").toString())

        change.setOnClickListener {
            change.visibility = View.GONE
            save.visibility = View.VISIBLE
            address.visibility = View.VISIBLE
        }

        save.setOnClickListener {
            sharedPreferences.edit().putString("address", address.text.toString()).apply()
            Toast.makeText(this, "Zapisano!", Toast.LENGTH_SHORT).show()
            save.visibility = View.GONE
            address.visibility = View.GONE
            change.visibility = View.VISIBLE
        }

        val callback = object : OnBackPressedCallback(true) {
            override fun handleOnBackPressed() {
                exitProcess(0)
            }
        }
        this.onBackPressedDispatcher.addCallback(this, callback)

        retry.callOnClick()
    }

    fun checkConnection(view: View) {
        retry.visibility = View.GONE
        progress.visibility = View.VISIBLE
        info.text = "Sprawdzam połączenie z ASTRO!"

        CoroutineScope(Dispatchers.IO).launch {
            delay(2500)
            if(!apiClient.connectionTest()){
                withContext(Dispatchers.Main){
                    info.text = "Nie można połączyć z ASTRO!"
                    retry.visibility = View.VISIBLE
                    change.visibility = View.VISIBLE
                    save.visibility = View.GONE
                    address.visibility = View.GONE
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
