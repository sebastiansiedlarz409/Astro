package com.example.astromobile.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.astromobile.R
import kotlinx.android.synthetic.main.menu_item.view.*

class MenuAdapter(
    context: Context,
    private val data: ArrayList<String>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val rowView: View = inflater.inflate(R.layout.menu_item, parent, false)

        if(position == 0){
            rowView.logo.setImageResource(R.drawable.ic_image_black_24dp)
        }
        else if(position == 1){
            rowView.logo.setImageResource(R.drawable.ic_camera_black_24dp)
        }
        else if(position == 2){
            rowView.logo.setImageResource(R.drawable.ic_blur_on_black_24dp)
        }
        else if(position == 3){
            rowView.logo.setImageResource(R.drawable.ic_web_black_24dp)
        }
        else{
            rowView.logo.setImageResource(R.drawable.ic_info_outline_black_24dp)
        }

        rowView.desc.text = data[position]

        return rowView
    }

    override fun getItem(position: Int): Any = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}