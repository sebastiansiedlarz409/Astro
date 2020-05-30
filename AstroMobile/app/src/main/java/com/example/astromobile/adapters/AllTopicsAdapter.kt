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
import com.example.astromobile.models.Topic
import com.example.astromobile.services.AuthService
import kotlinx.android.synthetic.main.topic_item.view.*
import kotlinx.android.synthetic.main.topic_item.view.author
import kotlinx.android.synthetic.main.topic_item.view.date
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.Response

class AllTopicsAdapter(
    private val context: Context,
    private val data: ArrayList<Topic>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    private lateinit var authService: AuthService
    private val apiClient = ApiClientForum()

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val rowView: View = inflater.inflate(R.layout.topic_item, parent, false)

        authService = AuthService.getAuthService()!!

        rowView.date.text = data[position].date
        rowView.author.text = data[position].user.userName
        rowView.open.text = data[position].title

        rowView.open.setOnClickListener {
            val intent = Intent(context, ShowTopicActivity::class.java)
            intent.putExtra("id", data[position].id)
            context.startActivity(intent)
        }

        rowView.delete.setOnClickListener {
            if(data[position].user.id == authService.getLoggedUser()!!.id || authService.getLoggedUserRole().equals("Administrator")){
                CoroutineScope(Dispatchers.IO).launch {
                    val response: Response = apiClient.deleteTopic(authService.getLoggedUserToken()!!.token, data[position].id.toString())

                    when (response.code) {
                        200 -> {
                            context.startActivity(Intent(context, ForumActivity::class.java))
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

        return rowView
    }

    override fun getItem(position: Int): Topic = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}