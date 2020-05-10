package com.example.astromobile

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.DisplayMetrics
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.activity_apod_details.*

class APODDetailsActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_apod_details)
        supportActionBar?.hide()

        val urlHd = intent.getStringExtra("urlHd")
        val url = intent.getStringExtra("url")
        val title = intent.getStringExtra("title")
        val author = intent.getStringExtra("author")
        val description = intent.getStringExtra("description")

        val displayMetrics = DisplayMetrics()
        windowManager.defaultDisplay.getMetrics(displayMetrics)

        Picasso.with(this)
            .load(url)
            .resize(displayMetrics.widthPixels, displayMetrics.widthPixels)
            .into(image)

        image.setOnClickListener {
            intent = Intent(this, ShowImageActivity::class.java)
            intent.putExtra("url", urlHd)
            startActivity(intent)
        }

        imageTitle.text = "Tytuł: $title"
        imageAuthor.text = "Autor: $author"
        imageDesc.text = "Opis: $description"
    }
}
