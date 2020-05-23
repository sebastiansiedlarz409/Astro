package com.example.astromobile.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.astromobile.R
import com.example.astromobile.models.Comment
import kotlinx.android.synthetic.main.comment_item.view.*

class CommentsAdapter(
    private val context: Context,
    private val data: ArrayList<Comment>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val rowView: View = inflater.inflate(R.layout.comment_item, parent, false)

        rowView.date.text = data[position].date.toString()
        rowView.author.text = data[position].user.userName
        rowView.content.text = data[position].content

        return rowView
    }

    override fun getItem(position: Int): Comment = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}