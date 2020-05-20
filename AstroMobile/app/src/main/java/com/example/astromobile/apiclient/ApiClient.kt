package com.example.astromobile.apiclient

import com.example.astromobile.models.*
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import org.json.JSONObject
import ru.gildor.coroutines.okhttp.await
import java.net.SocketTimeoutException
import java.util.concurrent.TimeUnit

class ApiClient {

    private val JSON = "application/json; charset=utf-8".toMediaTypeOrNull()

    private val urlMain: String = "http://192.168.1.2:5001"
    private val urlEPIC: String = "api/EPIC"
    private val urlAPOD: String = "api/APOD"
    private val urlInsight: String = "api/Insight"
    private val urlAsteroidsNeoWs: String = "api/AsteroidsNeoWs"
    private val urlLogin: String = "api/Auth/Login"
    private val urlRegister: String = "api/Auth/Register"
    private val urlAllTopics: String = "api/APIForum/Topic"

    private var client: OkHttpClient = OkHttpClient.Builder()
        .connectTimeout(2000, TimeUnit.MILLISECONDS)
        .connectionSpecs(arrayListOf(ConnectionSpec.MODERN_TLS, ConnectionSpec.CLEARTEXT)).build()

    suspend fun connectionTest() : Boolean{
        try{
            val request = Request.Builder()
                .url(urlMain)
                .build()

            client.newCall(request).await()
        }
        catch (ex: SocketTimeoutException){
            return false
        }

        return true
    }

    //region Auth

    suspend fun login(email: String, password: String): Response{
        val data = JSONObject()
        data.put("Email", email)
        data.put("Password", password)

        val request = Request.Builder()
            .url("$urlMain/$urlLogin")
            .post(RequestBody.create(JSON, data.toString()))
            .build()

        return client.newCall(request).await()
    }

    suspend fun loginData(data: String?): Token{
        return Gson().fromJson(data, object : TypeToken<Token>() {}.type)
    }

    suspend fun register(username: String, email: String, password: String, passwordConfirm: String): Response{
        val data = JSONObject()
        data.put("UserName", username)
        data.put("Email", email)
        data.put("Password", password)
        data.put("ConfirmPassword", passwordConfirm)

        val request = Request.Builder()
            .url("$urlMain/$urlRegister")
            .post(RequestBody.create(JSON, data.toString()))
            .build()

        return client.newCall(request).await()
    }

    //endregion

    // region Forum

    suspend fun getAllTopics(token: String) : Response {
        val request = Request.Builder()
            .url("$urlMain/$urlAllTopics")
            .addHeader("Authorization", "Bearer $token")
            .build()

        return client.newCall(request).await()
    }

    fun getAllTopicsData(data: String?): MutableList<Topic>{
        println(data?.subSequence(0,40))
        return Gson().fromJson(data, object : TypeToken<MutableList<Topic>>() {}.type)
    }

    suspend fun postTopic(token: String,id: String, title: String, content: String) : Response {
        val data = JSONObject()
        data.put("UserId", id)
        data.put("Topic", title)
        data.put("Comment", content)

        val request = Request.Builder()
            .url("$urlMain/$urlAllTopics")
            .addHeader("Authorization", "Bearer $token")
            .post(RequestBody.create(JSON, data.toString()))
            .build()

        return client.newCall(request).await()
    }

    // endregion

    //region NASA API

    suspend fun getAPODList() : String? {
        val request = Request.Builder()
            .url("$urlMain/$urlAPOD")
            .build()

        val response: Response = client.newCall(request).await()

        return withContext(Dispatchers.IO) { response.body?.string() }
    }

    fun getAPODListData(data: String?): MutableList<APOD>{
        return Gson().fromJson(data, object : TypeToken<MutableList<APOD>>() {}.type)
    }

    suspend fun getEPICList() : String? {
        val request = Request.Builder()
            .url("$urlMain/$urlEPIC")
            .build()

        val response: Response = client.newCall(request).await()

        return withContext(Dispatchers.IO) { response.body?.string() }
    }

    fun getEPICListData(data: String?): MutableList<EPIC>{
        return Gson().fromJson(data, object : TypeToken<MutableList<EPIC>>() {}.type)
    }

    suspend fun getAsteroidsNeoWsList() : String? {
        val request = Request.Builder()
            .url("$urlMain/$urlAsteroidsNeoWs")
            .build()

        val response: Response = client.newCall(request).await()

        return withContext(Dispatchers.IO) { response.body?.string() }
    }

    fun getAsteroidsNeoWsListData(data: String?): MutableList<AsteroidsNeoWs>{
        return Gson().fromJson(data, object : TypeToken<MutableList<AsteroidsNeoWs>>() {}.type)
    }

    suspend fun getInsightList() : String? {
        val request = Request.Builder()
            .url("$urlMain/$urlInsight")
            .build()

        val response: Response = client.newCall(request).await()

        return withContext(Dispatchers.IO) { response.body?.string() }
    }

    fun getInsightListData(data: String?): MutableList<Insight>{
        return Gson().fromJson(data, object : TypeToken<MutableList<Insight>>() {}.type)
    }

    //endregion
}