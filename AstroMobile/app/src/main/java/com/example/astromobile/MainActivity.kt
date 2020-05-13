package com.example.astromobile

import android.content.Intent
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.activity.OnBackPressedCallback
import androidx.appcompat.app.AlertDialog
import com.example.astromobile.adapters.MenuAdapter
import com.example.astromobile.apiclient.ApiClient
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.android.synthetic.main.connection_error.*
import kotlinx.coroutines.*
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.Dispatchers.Main
import kotlin.system.exitProcess

class MainActivity : AppCompatActivity() {

    private val apiClient = ApiClient()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        supportActionBar?.hide()

        setContentView(R.layout.connection_error)
        retry.visibility = View.GONE
        progress.visibility = View.VISIBLE

        val callback = object : OnBackPressedCallback(true) {
            override fun handleOnBackPressed() {
                exitProcess(0)
            }
        }
        this.onBackPressedDispatcher.addCallback(this, callback)

        checkConnection()
    }

    private fun checkConnection(){
        CoroutineScope(IO).launch {
            delay(4000)
            if(!apiClient.connectionTest()){
                withContext(Main){
                    info.text = "Nie można połączyć ze źródłem danych!"
                    retry.visibility = View.VISIBLE
                    progress.visibility = View.GONE
                }
            }
            else{
                withContext(Main){
                    setContentView(R.layout.activity_main)
                    mainPage()
                }
            }
        }
    }

    private fun mainPage() {
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
            else if(position == 4){

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

    fun retry(view: View){
        val intent = Intent(this, MainActivity::class.java)
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)
        startActivity(intent)
    }
}
