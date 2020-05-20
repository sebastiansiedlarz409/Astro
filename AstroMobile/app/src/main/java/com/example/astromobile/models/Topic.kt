package com.example.astromobile.models

class Topic(var id: Int, var rate: Int, var title: String, var date: String, var user: User,
            var comments: MutableList<Comment>)