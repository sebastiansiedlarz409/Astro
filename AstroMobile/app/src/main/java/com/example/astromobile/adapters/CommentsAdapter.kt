package com.example.astromobile.adapters

import android.annotation.SuppressLint
import android.app.AlertDialog
import android.content.Context
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.astromobile.*
import com.example.astromobile.apiclient.ApiClientForum
import com.example.astromobile.models.Comment
import com.example.astromobile.services.AuthService
import kotlinx.android.synthetic.main.comment_item.view.*
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.launch
import okhttp3.Response

class CommentsAdapter(
    private val id: Int,
    private val context: Context,
    private val data: ArrayList<Comment>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    private lateinit var authService: AuthService
    private val apiClient = ApiClientForum()

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val commentsItem: View = inflater.inflate(R.layout.comment_item, parent, false)

        authService = AuthService.getAuthService()!!

        commentsItem.date.text = data[position].date.toString()
        commentsItem.author.text = data[position].user.userName
        commentsItem.content.text = data[position].content

        commentsItem.content.setOnClickListener {
            if(data[position].user.id.equals(authService.getLoggedUser()!!.id)){
                val intent = Intent(context, AddCommentActivity::class.java)
                intent.putExtra("id", data[position].id)
                intent.putExtra("content", data[position].content)
                context.startActivity(intent)
            }
        }

        commentsItem.delete.setOnClickListener {
            if(data[position].user.id.equals(authService.getLoggedUser()!!.id)){
                CoroutineScope(IO).launch {
                    val response: Response = apiClient.deleteComment(authService.getLoggedUserToken()!!.token, data[position].id.toString())

                    when (response.code) {
                        200 -> {
                            val intent = Intent(context, ShowTopicActivity::class.java)
                            intent.putExtra("id", id)
                            context.startActivity(intent)
                        }
                        401 -> {
                            val builder = AlertDialog.Builder(context, R.style.InfoAlert)
                            builder.setTitle("Forum")
                            builder.setMessage("Musisz się zalogować!")
                            builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                            builder.setPositiveButton("Rejestruj") { _, _ ->
                                context.startActivity(Intent(context, RegisterActivity::class.java))
                            }
                            builder.setNegativeButton("Zaloguj") { _, _ ->
                                context.startActivity(Intent(context, LoginActivity::class.java))
                            }
                            builder.show()
                        }
                        else -> {
                            val builder = AlertDialog.Builder(context, R.style.InfoAlert)
                            builder.setTitle("Forum")
                            builder.setMessage("Wystąpił nie oczekiwany bład!")
                            builder.setIcon(R.drawable.ic_info_outline_black_24dp)
                            builder.setPositiveButton("OK") { _, _ ->
                                context.startActivity(Intent(context, ConnectionCheckActivity::class.java))
                            }
                            builder.show()
                        }
                    }
                }
            }
        }

        return commentsItem
    }

    override fun getItem(position: Int): Comment = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}