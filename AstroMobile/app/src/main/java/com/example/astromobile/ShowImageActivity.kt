package com.example.astromobile

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.activity_show_image.*

class ShowImageActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_show_image)
        supportActionBar?.hide()

        val url = intent.getStringExtra("url")

        Picasso.get()
            .load(url)
            .into(image)
    }
}
