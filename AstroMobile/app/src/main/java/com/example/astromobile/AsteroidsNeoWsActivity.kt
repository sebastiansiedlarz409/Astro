package com.example.astromobile

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.astromobile.adapters.AsteroidsNeoWsAdapter
import com.example.astromobile.apiclient.ApiClient
import com.example.astromobile.models.AsteroidsNeoWs
import kotlinx.android.synthetic.main.activity_asteroids_neo_ws.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class AsteroidsNeoWsActivity : AppCompatActivity() {

    private val apiClient = ApiClient()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_asteroids_neo_ws)
        supportActionBar?.hide()

        var adapter = AsteroidsNeoWsAdapter(this, arrayListOf())
        val listItems: ArrayList<AsteroidsNeoWs> = arrayListOf()

        CoroutineScope(Dispatchers.IO).launch{
            val data: String? = apiClient.getAsteroidsNeoWsList()
            val asteroidsNeoWsList: MutableList<AsteroidsNeoWs> = apiClient.getAsteroidsNeoWsListData(data)

            for(item in asteroidsNeoWsList){
                listItems.add(item)
            }

            withContext(Dispatchers.Main){
                asteroids.adapter = adapter
            }
        }

        adapter = AsteroidsNeoWsAdapter(this, listItems)
        asteroids.adapter = adapter
    }
}
