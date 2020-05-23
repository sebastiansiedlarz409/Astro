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

open class ApiClient {

    protected val JSON = "application/json; charset=utf-8".toMediaTypeOrNull()
    protected val urlMain: String = "http://192.168.1.2:5001"

    protected var client: OkHttpClient = OkHttpClient.Builder()
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
}