package com.example.astromobile.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.astromobile.R
import com.example.astromobile.models.Topic
import kotlinx.android.synthetic.main.topic_item.view.*

class AllTopicsAdapter(
    private val context: Context,
    private val data: ArrayList<Topic>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val rowView: View = inflater.inflate(R.layout.topic_item, parent, false)

        rowView.date.text = data[position].date
        rowView.author.text = data[position].user.userName
        rowView.open.text = data[position].title

        return rowView
    }

    override fun getItem(position: Int): Topic = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}