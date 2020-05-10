package com.example.astromobile.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.astromobile.R
import com.example.astromobile.models.APOD
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.apod_item.view.*

class APODAdapter(
    private val context: Context,
    private val data: ArrayList<APOD>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val rowView: View = inflater.inflate(R.layout.apod_item, parent, false)

        Picasso.with(this.context)
            .load(data[position].url)
            .resize(80, 80)
            .centerCrop()
            .into(rowView.image)

        rowView.titleAPOD.text = data[position].date

        return rowView
    }

    override fun getItem(position: Int): APOD = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}