package com.example.astromobile

import android.content.Context
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.astromobile.adapters.InsightAdapter
import com.example.astromobile.apiclient.ApiClient
import com.example.astromobile.apiclient.ApiClientNasa
import com.example.astromobile.models.Insight
import kotlinx.android.synthetic.main.activity_insight.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class InsightActivity : AppCompatActivity() {

    private lateinit var apiClient: ApiClientNasa
    private lateinit var sharedPreferences: SharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_insight)
        supportActionBar?.hide()

        sharedPreferences = getSharedPreferences("ASTRO", Context.MODE_PRIVATE);
        apiClient = ApiClientNasa(sharedPreferences)

        //data is no longer available
        return;

        var adapter = InsightAdapter(this, arrayListOf())
        val listItems: ArrayList<Insight> = arrayListOf()

        CoroutineScope(Dispatchers.IO).launch{
            val data: String? = apiClient.getInsightList()
            val insightList: MutableList<Insight> = apiClient.getInsightListData(data)

            for(item in insightList){
                listItems.add(item)
            }

            withContext(Dispatchers.Main){
                insight.adapter = adapter
            }
        }

        adapter = InsightAdapter(this, listItems)
        insight.adapter = adapter
    }
}
