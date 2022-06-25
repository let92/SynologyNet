﻿using Microsoft.Extensions.Configuration;
using SynologyNet;
using System;
using System.Linq;

var config = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddUserSecrets<Program>()
    .Build();

var host = config["Synology:Host"];
var username = config["Synology:Username"];
var password = config["Synology:Password"];
var synology = new Synology(host, username, password);

Console.WriteLine($"Logged in: {synology.Authentication.IsAuthenticated}");
Console.WriteLine(await synology.Authentication.Login());
Console.WriteLine($"Logged in: {synology.Authentication.IsAuthenticated}");

Console.WriteLine("Recently added photos");
Console.WriteLine(await synology.Photo.GetRecentlyAddedPhotos());
Console.WriteLine();

var albums = await synology.Photo.GetSharedAlbums();
var photos = await synology.Photo.GetPhotosFromAlbum(albums.First());

Console.WriteLine(await synology.Authentication.Logout());
Console.WriteLine($"Logged in: {synology.Authentication.IsAuthenticated}");

Console.WriteLine("Waiting for user input..");
Console.ReadKey();