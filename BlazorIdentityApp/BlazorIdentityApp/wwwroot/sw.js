
'use strict';

self.addEventListener('push', function (event) {
    console.log('[Service Worker] Push Received.');

    let data = {};
    if (event.data) {
        console.log(data);
        data = event.data.json();  // Extracting the JSON data from the event
    }
    const title = data.title;
    const options = {
        body: data.body,
        icon: 'images/icon.png',
        badge: 'images/badge.png'
    };

    event.waitUntil(self.registration.showNotification(title, options));
});