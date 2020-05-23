package com.example.astromobile.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.astromobile.AddCommentActivity
import com.example.astromobile.R
import com.example.astromobile.models.Comment
import com.example.astromobile.services.AuthService
import kotlinx.android.synthetic.main.comment_item.view.*

class CommentsAdapter(
    private val context: Context,
    private val data: ArrayList<Comment>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    private lateinit var authService: AuthService

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val rowView: View = inflater.inflate(R.layout.comment_item, parent, false)

        authService = AuthService.getAuthService()!!

        rowView.date.text = data[position].date.toString()
        rowView.author.text = data[position].user.userName
        rowView.content.text = data[position].content

        rowView.content.setOnClickListener {
            if(data[position].user.id.equals(authService.getLoggedUser()!!.id)){
                val intent = Intent(context, AddCommentActivity::class.java)
                intent.putExtra("id", data[position].id)
                intent.putExtra("content", data[position].content)
                context.startActivity(intent)
            }
        }

        return rowView
    }

    override fun getItem(position: Int): Comment = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}