package com.example.astromobile.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.net.Uri
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.astromobile.R
import com.example.astromobile.models.AsteroidsNeoWs
import kotlinx.android.synthetic.main.asteroidsneows_item.view.*

class AsteroidsNeoWsAdapter(
    private val context: Context,
    private val data: ArrayList<AsteroidsNeoWs>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val rowView: View = inflater.inflate(R.layout.asteroidsneows_item, parent, false)

        rowView.name.text = data[position].name
        rowView.size.text = data[position].size
        rowView.fobservation.text = data[position].firstObservation
        rowView.lobservation.text = data[position].lastObservation

        rowView.link.setOnClickListener {
             context.startActivity(Intent(Intent.ACTION_VIEW, Uri.parse(data[position].url)))
        }

        if(data[position].dangerous == "True"){
            rowView.name.setTextColor(Integer.parseUnsignedInt("ffff0000",16))
        }

        return rowView
    }

    override fun getItem(position: Int): AsteroidsNeoWs = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}