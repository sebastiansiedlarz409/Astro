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
        val insightItem: View = inflater.inflate(R.layout.insight_item, parent, false)

        insightItem.date.text = "Data: ${data[position].date}"
        insightItem.season.text = "Pora roku: ${data[position].season}"
        insightItem.minPress.text = data[position].minPress
        insightItem.avgPress.text = data[position].avgPress
        insightItem.maxPress.text = data[position].maxPress
        insightItem.minTemp.text = data[position].minTemp
        insightItem.avgTemp.text = data[position].avgTemp
        insightItem.maxTemp.text = data[position].maxTemp
        insightItem.minWind.text = data[position].minWind
        insightItem.avgWind.text = data[position].avgWind
        insightItem.maxWind.text = data[position].maxWind

        return insightItem
    }

    override fun getItem(position: Int): Insight = data[position]

    override fun getItemId(position: Int): Long = position.toLong()

    override fun getCount(): Int = data.size
}