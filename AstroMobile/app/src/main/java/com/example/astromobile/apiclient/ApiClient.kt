package com.example.astromobile.apiclient

import android.content.SharedPreferences
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import ru.gildor.coroutines.okhttp.await
import java.net.SocketTimeoutException
import java.util.concurrent.TimeUnit

open class ApiClient(sharedPreferences: SharedPreferences) {

    protected val JSON = "application/json; charset=utf-8".toMediaTypeOrNull()
    protected var urlMain: String = "http://192.168.1.2:5001"

    protected var client: OkHttpClient = OkHttpClient.Builder()
        .connectTimeout(2000, TimeUnit.MILLISECONDS)
        .connectionSpecs(arrayListOf(ConnectionSpec.MODERN_TLS, ConnectionSpec.CLEARTEXT)).build()

    init {
        urlMain = "http://"+sharedPreferences.getString("address", "192.168.1.2:5001");
    }

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
        catch (ex: IllegalArgumentException){
            return false
        }

        return true
    }
}