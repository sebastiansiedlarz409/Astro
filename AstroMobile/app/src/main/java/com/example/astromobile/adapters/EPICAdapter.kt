package com.example.astromobile.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.astromobile.R
import com.example.astromobile.models.EPIC
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.epic_item.view.*

class EPICAdapter(
    private val context: Context,
    private val data: ArrayList<EPIC>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val rowView: View = inflater.inflate(R.layout.epic_item, parent, false)

        Picasso.get()
            .load(data[0].imageName)
            .resize(80, 80)
            .centerCrop()
            .into(rowView.image)

        rowView.dateEPIC.text = data[position].date

        return rowView
    }

    override fun getItem(position: Int): EPIC = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}