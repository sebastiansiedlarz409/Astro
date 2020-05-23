package com.example.astromobile

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

    private val apiClient = ApiClientNasa()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_insight)
        supportActionBar?.hide()

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
