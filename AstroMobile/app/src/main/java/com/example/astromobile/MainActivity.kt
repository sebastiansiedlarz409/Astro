package com.example.astromobile

import android.content.Intent
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.appcompat.app.AlertDialog
import com.example.astromobile.adapters.MenuAdapter
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        supportActionBar?.hide()

        val options: ArrayList<String> = arrayListOf("APOD", "EPIC", "Asteroids", "Insight", "Informacje")

        val adapter = MenuAdapter(this, options)

        menu.adapter = adapter

        menu.setOnItemClickListener{
                _, _, position, _ ->
            if(position == 0){
                startActivity(Intent(this, APODActivity::class.java))
            }
            else if(position == 1){

            }
            else if(position == 2){

            }
            else if(position == 3){

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
