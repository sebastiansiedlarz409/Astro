package com.example.astromobile

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.astromobile.adapters.EPICAdapter
import com.example.astromobile.apiclient.ApiClient
import com.example.astromobile.apiclient.ApiClientNasa
import com.example.astromobile.models.EPIC
import kotlinx.android.synthetic.main.activity_epic.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class EPICActivity : AppCompatActivity() {

    private lateinit var apiClient: ApiClientNasa
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_epic)
        supportActionBar?.hide()
        sharedPreferences = getSharedPreferences("ASTRO", Context.MODE_PRIVATE);
        apiClient = ApiClientNasa(sharedPreferences)

        var adapter = EPICAdapter(this, arrayListOf())
        val listItems: ArrayList<EPIC> = arrayListOf()

        CoroutineScope(Dispatchers.IO).launch{
            val data: String? = apiClient.getEPICList()
            val epicList: MutableList<EPIC> = apiClient.getEPICListData(data)

            for(item in epicList){
                listItems.add(item)
            }

            withContext(Dispatchers.Main){
                epic.adapter = adapter
            }
        }

        adapter = EPICAdapter(this, listItems)
        epic.adapter = adapter

        epic.setOnItemClickListener {
                _, _, position, _ ->
            val intent = Intent(this, ShowImageActivity::class.java)
            intent.putExtra("url", adapter.getItem(position).imageName)
            startActivity(intent)
        }
    }
}
