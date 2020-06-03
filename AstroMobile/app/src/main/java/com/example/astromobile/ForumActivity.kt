package com.example.astromobile

import android.app.AlertDialog
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.activity.OnBackPressedCallback
import com.example.astromobile.adapters.AllTopicsAdapter
import com.example.astromobile.apiclient.ApiClient
import com.example.astromobile.apiclient.ApiClientForum
import com.example.astromobile.models.Topic
import com.example.astromobile.services.AuthService
import kotlinx.android.synthetic.main.activity_forum.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import okhttp3.Response

class ForumActivity : AppCompatActivity() {

    private lateinit var apiClient: ApiClientForum
    private lateinit var sharedPreferences: SharedPreferences
    private lateinit var authService: AuthService

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_forum)
        supportActionBar?.hide()

        sharedPreferences = getSharedPreferences("ASTRO", Context.MODE_PRIVATE);
        authService = AuthService.getAuthService(this)!!

        apiClient = ApiClientForum(sharedPreferences)

        val callback = object : OnBackPressedCallback(true) {
            override fun handleOnBackPressed() {
                startActivity(Intent(this@ForumActivity, MainActivity::class.java))
            }
        }
        this.onBackPressedDispatcher.addCallback(this, callback)

        var adapter = AllTopicsAdapter(this, arrayListOf())
        val listItems: ArrayList<Topic> = arrayListOf()

        CoroutineScope(Dispatchers.IO).launch{
            val response: Response = apiClient.getAllTopics(authService.getLoggedUserToken()!!.token)

            when (response.code) {
                200 -> {

                    val topicsList: MutableList<Topic> = apiClient.getAllTopicsData(response.body?.string())

                    for(item in topicsList){
                        listItems.add(item)
                    }

                    withContext(Dispatchers.Main){
                        topics.adapter = adapter
                    }
                }
                401 -> {
                    startActivity(Intent(this@ForumActivity, LoginActivity::class.java))
                }
                else -> {
                    val builder = AlertDialog.Builder(this@ForumActivity, R.style.InfoAlert)
                    builder.setTitle("Forum")
                    builder.setMessage("Wystąpił nie oczekiwany bład!")
                    builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                    builder.setPositiveButton("OK") { _, _ ->
                        startActivity(Intent(this@ForumActivity, ConnectionCheckActivity::class.java))
                    }
                    builder.show()
                }
            }
        }

        adapter = AllTopicsAdapter(this, listItems)
        topics.adapter = adapter

        addTopic.setOnClickListener {
            if(authService.isLogged()){
                startActivity(Intent(this@ForumActivity, AddTopicActivity::class.java))
            }
            else{
                val builder = AlertDialog.Builder(this, R.style.InfoAlert)
                builder.setTitle("Forum")
                builder.setMessage("Musisz się zalogować!")
                builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                builder.setPositiveButton("Rejestruj") { _, _ ->
                    startActivity(Intent(this@ForumActivity, RegisterActivity::class.java))
                }
                builder.setNegativeButton("Zaloguj") { _, _ ->
                    startActivity(Intent(this@ForumActivity, LoginActivity::class.java))
                }
                builder.show()
            }
        }
    }
}
