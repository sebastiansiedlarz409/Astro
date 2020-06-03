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
        val menuItem: View = inflater.inflate(R.layout.menu_item, parent, false)

        when (position) {
            0 -> {
                menuItem.logo.setImageResource(R.drawable.ic_image_black_24dp)
            }
            1 -> {
                menuItem.logo.setImageResource(R.drawable.ic_camera_black_24dp)
            }
            2 -> {
                menuItem.logo.setImageResource(R.drawable.ic_blur_on_black_24dp)
            }
            3 -> {
                menuItem.logo.setImageResource(R.drawable.ic_web_black_24dp)
            }
            4 -> {
                menuItem.logo.setImageResource(R.drawable.ic_forum_black_24dp)
            }
            else -> {
                menuItem.logo.setImageResource(R.drawable.ic_info_outline_black_24dp)
            }
        }

        menuItem.desc.text = data[position]

        return menuItem
    }

    override fun getItem(position: Int): Any = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}