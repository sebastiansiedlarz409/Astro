package com.example.astromobile.apiclient

import android.content.SharedPreferences
import com.example.astromobile.models.Topic
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import okhttp3.Request
import okhttp3.RequestBody
import okhttp3.Response
import org.json.JSONObject
import ru.gildor.coroutines.okhttp.await

class ApiClientForum(sharedPreferences: SharedPreferences): ApiClient(sharedPreferences){

    private val urlTopics: String = "api/APIForum/Topic"
    private val urlComments: String = "api/APIForum/Comment"
    private val urlRate: String = "api/APIForum/Rate"

    suspend fun getAllTopics(token: String) : Response {
        val request = Request.Builder()
            .url("$urlMain/$urlTopics")
            .addHeader("Authorization", "Bearer $token")
            .build()

        return client.newCall(request).await()
    }

    fun getAllTopicsData(data: String?): MutableList<Topic>{
        return Gson().fromJson(data, object : TypeToken<MutableList<Topic>>() {}.type)
    }

    suspend fun getTopic(token: String, id: Int) : Response {
        val request = Request.Builder()
            .url("$urlMain/$urlTopics/$id")
            .addHeader("Authorization", "Bearer $token")
            .build()

        return client.newCall(request).await()
    }

    fun getTopicData(data: String?): Topic {
        return Gson().fromJson(data, object : TypeToken<Topic>() {}.type)
    }

    suspend fun postTopic(token: String, userId: String, title: String, content: String) : Response {
        val data = JSONObject()
        data.put("UserId", userId)
        data.put("Topic", title)
        data.put("Comment", content)

        val request = Request.Builder()
            .url("$urlMain/$urlTopics")
            .addHeader("Authorization", "Bearer $token")
            .post(RequestBody.create(JSON, data.toString()))
            .build()

        return client.newCall(request).await()
    }

    suspend fun postComment(token: String, userId: String, topicId: String, comment: String) : Response {
        val data = JSONObject()
        data.put("UserId", userId)
        data.put("TopicId", topicId)
        data.put("Comment", comment)

        val request = Request.Builder()
            .url("$urlMain/$urlComments")
            .addHeader("Authorization", "Bearer $token")
            .post(RequestBody.create(JSON, data.toString()))
            .build()

        return client.newCall(request).await()
    }

    suspend fun editComment(token: String, commentId: String, comment: String) : Response {
        val data = JSONObject()
        data.put("Id", commentId)
        data.put("Comment", comment)

        val request = Request.Builder()
            .url("$urlMain/$urlComments")
            .addHeader("Authorization", "Bearer $token")
            .put(RequestBody.create(JSON, data.toString()))
            .build()

        return client.newCall(request).await()
    }

    suspend fun deleteComment(token: String, commentId: String) : Response {

        val request = Request.Builder()
            .url("$urlMain/$urlComments/$commentId")
            .addHeader("Authorization", "Bearer $token")
            .delete(RequestBody.create(JSON, "{}"))
            .build()

        return client.newCall(request).await()
    }

    suspend fun deleteTopic(token: String, topicId: String) : Response {

        val request = Request.Builder()
            .url("$urlMain/$urlTopics/$topicId")
            .addHeader("Authorization", "Bearer $token")
            .delete(RequestBody.create(JSON, "{}"))
            .build()

        return client.newCall(request).await()
    }

    suspend fun rateTopic(token: String, topicId: String, rate: Int): Response {

        val request = Request.Builder()
            .url("$urlMain/$urlRate/$topicId/$rate")
            .addHeader("Authorization", "Bearer $token")
            .put(RequestBody.create(JSON, "{}"))
            .build()

        return client.newCall(request).await()
    }
}