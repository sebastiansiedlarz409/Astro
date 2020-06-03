package com.example.astromobile

import android.app.AlertDialog
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.astromobile.apiclient.ApiClient
import com.example.astromobile.apiclient.ApiClientForum
import com.example.astromobile.services.AuthService
import kotlinx.android.synthetic.main.activity_add_topic.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.launch
import okhttp3.Response

class AddTopicActivity : AppCompatActivity() {

    private lateinit var apiClient: ApiClientForum
    private lateinit var sharedPreferences: SharedPreferences
    private lateinit var authService: AuthService

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_add_topic)
        supportActionBar?.hide()

        sharedPreferences = getSharedPreferences("ASTRO", Context.MODE_PRIVATE);
        authService = AuthService.getAuthService(this)!!

        apiClient = ApiClientForum(sharedPreferences)

        addTopic.setOnClickListener {
            val title: String = topic.text.toString()
            val content: String = content.text.toString()

            CoroutineScope(IO).launch {
                val response: Response = apiClient.postTopic(authService.getLoggedUserToken()!!.token,
                    authService.getLoggedUser()!!.id, title, content)

                when (response.code) {
                    200 -> {
                        startActivity(Intent(this@AddTopicActivity, ForumActivity::class.java))
                    }
                    401 -> {
                        val builder = AlertDialog.Builder(this@AddTopicActivity, R.style.InfoAlert)
                        builder.setTitle("Forum")
                        builder.setMessage("Musisz się zalogować!")
                        builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                        builder.setPositiveButton("Rejestruj") { _, _ ->
                            startActivity(Intent(this@AddTopicActivity, RegisterActivity::class.java))
                        }
                        builder.setNegativeButton("Zaloguj") { _, _ ->
                            startActivity(Intent(this@AddTopicActivity, LoginActivity::class.java))
                        }
                        builder.show()
                    }
                    else -> {
                        val builder = AlertDialog.Builder(this@AddTopicActivity, R.style.InfoAlert)
                        builder.setTitle("Forum")
                        builder.setMessage("Wystąpił nie oczekiwany bład!")
                        builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                        builder.setPositiveButton("OK") { _, _ ->
                            startActivity(Intent(this@AddTopicActivity, ConnectionCheckActivity::class.java))
                        }
                        builder.show()
                    }
                }
            }
        }
    }
}
