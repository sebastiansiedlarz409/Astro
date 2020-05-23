package com.example.astromobile

import android.app.AlertDialog
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.astromobile.apiclient.ApiClient
import com.example.astromobile.apiclient.ApiClientForum
import com.example.astromobile.services.AuthService
import kotlinx.android.synthetic.main.activity_add_comment.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.Response

class AddCommentActivity : AppCompatActivity() {

    private lateinit var authService: AuthService
    private val apiClient = ApiClientForum()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_add_comment)
        supportActionBar?.hide()

        authService = AuthService.getAuthService()!!

        val id: Int = intent.getIntExtra("id", 0)
        val comment: String? = intent.getStringExtra("content")

        if(comment != null){
            content.setText(comment.toString())
        }

        addComment.setOnClickListener {
            val content = content.text.toString()

            CoroutineScope(Dispatchers.IO).launch {

                val response: Response

                if(comment == null){
                    response = apiClient.postComment(authService.getLoggedUserToken()!!.token,
                        authService.getLoggedUser()!!.id, id.toString(), content)
                }
                else{
                    response = apiClient.editComment(authService.getLoggedUserToken()!!.token,
                        id.toString(), content)
                }

                when (response.code) {
                    200 -> {
                        val intent = Intent(this@AddCommentActivity, ShowTopicActivity::class.java)
                        intent.putExtra("id", id)
                        startActivity(intent)
                    }
                    401 -> {
                        val builder = AlertDialog.Builder(this@AddCommentActivity, R.style.InfoAlert)
                        builder.setTitle("Forum")
                        builder.setMessage("Musisz się zalogować!")
                        builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                        builder.setPositiveButton("Rejestruj") { _, _ ->
                            startActivity(Intent(this@AddCommentActivity, RegisterActivity::class.java))
                        }
                        builder.setNegativeButton("Zaloguj") { _, _ ->
                            startActivity(Intent(this@AddCommentActivity, LoginActivity::class.java))
                        }
                        builder.show()
                    }
                    else -> {
                        val builder = AlertDialog.Builder(this@AddCommentActivity, R.style.InfoAlert)
                        builder.setTitle("Forum")
                        builder.setMessage("Wystąpił nie oczekiwany bład!")
                        builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                        builder.setPositiveButton("OK") { _, _ ->
                            startActivity(Intent(this@AddCommentActivity, ConnectionCheckActivity::class.java))
                        }
                        builder.show()
                    }
                }
            }

        }
    }
}
