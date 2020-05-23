package com.example.astromobile

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.astromobile.adapters.APODAdapter
import com.example.astromobile.apiclient.ApiClient
import com.example.astromobile.apiclient.ApiClientNasa
import com.example.astromobile.models.APOD
import kotlinx.android.synthetic.main.activity_apod.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.Dispatchers.Main
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class APODActivity : AppCompatActivity() {

    private val apiClient = ApiClientNasa()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_apod)
        supportActionBar?.hide()

        var adapter = APODAdapter(this, arrayListOf())
        val listItems: ArrayList<APOD> = arrayListOf()

        CoroutineScope(IO).launch{
            val data: String? = apiClient.getAPODList()
            val apodList: MutableList<APOD> = apiClient.getAPODListData(data)

            for(item in apodList){
                listItems.add(item)
            }

            withContext(Main){
                apod.adapter = adapter
            }
        }

        adapter = APODAdapter(this, listItems)
        apod.adapter = adapter

        apod.setOnItemClickListener {
                _, _, position, _ ->
            val intent = Intent(this, APODDetailsActivity::class.java)
            intent.putExtra("urlHd", adapter.getItem(position).urlHd)
            intent.putExtra("url", adapter.getItem(position).url)
            intent.putExtra("title", adapter.getItem(position).title)
            intent.putExtra("author", adapter.getItem(position).author)
            intent.putExtra("description", adapter.getItem(position).description)
            startActivity(intent)
        }
    }
}
