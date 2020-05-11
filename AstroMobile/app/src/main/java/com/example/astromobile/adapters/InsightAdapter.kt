package com.example.astromobile.adapters

import android.annotation.SuppressLint
import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import com.example.astromobile.R
import com.example.astromobile.models.Insight
import kotlinx.android.synthetic.main.insight_item.view.*

class InsightAdapter(
    private val context: Context,
    private val data: ArrayList<Insight>): BaseAdapter() {

    private val inflater: LayoutInflater
            = context.getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater

    @SuppressLint("ViewHolder")
    override fun getView(position: Int, convertView: View?, parent: ViewGroup?): View {
        val rowView: View = inflater.inflate(R.layout.insight_item, parent, false)

        rowView.date.text = "Data: ${data[position].date}"
        rowView.season.text = "Pora roku: ${data[position].season}"
        rowView.minPress.text = data[position].minPress
        rowView.avgPress.text = data[position].avgPress
        rowView.maxPress.text = data[position].maxPress
        rowView.minTemp.text = data[position].minTemp
        rowView.avgTemp.text = data[position].avgTemp
        rowView.maxTemp.text = data[position].maxTemp
        rowView.minWind.text = data[position].minWind
        rowView.avgWind.text = data[position].avgWind
        rowView.maxWind.text = data[position].maxWind

        return rowView
    }

    override fun getItem(position: Int): Insight = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}