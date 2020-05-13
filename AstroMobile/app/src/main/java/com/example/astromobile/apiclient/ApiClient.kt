package com.example.astromobile.apiclient

import com.example.astromobile.models.APOD
import com.example.astromobile.models.AsteroidsNeoWs
import com.example.astromobile.models.EPIC
import com.example.astromobile.models.Insight
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import okhttp3.ConnectionSpec
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.Response
import ru.gildor.coroutines.okhttp.await
import java.net.SocketTimeoutException
import java.util.concurrent.TimeUnit

class ApiClient {

    private val urlMain: String = "http://192.168.1.2:5001"
    private val urlEPIC: String = "api/EPIC"
    private val urlAPOD: String = "api/APOD"
    private val urlInsight: String = "api/Insight"
    private val urlAsteroidsNeoWs: String = "api/AsteroidsNeoWs"
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
}