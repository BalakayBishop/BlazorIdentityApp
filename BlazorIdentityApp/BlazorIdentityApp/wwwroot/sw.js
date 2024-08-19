'use strict';

//listen to any push event
//listen to any push event
self.addEventListener('push', function (event) {
    let payload
    try {
        // parse JSON string to an object
        payload = JSON.parse(event.data.text())
    } catch (err) {
        console.log(err)
        // if error in parsing we can set default value to the payload
        payload = {
            title: 'New Updates Arrives!',
            body: 'We Got Something for You!',
            clickUrl: ''
        }
    }
    const title = payload.title
    // this option is used to modify the notification 
    const options = {
        // this is the notification body
        body: payload.body || 'New Updates Arrives!',
        // notification icon
        icon: '/favicon.png',
        // badge icon
        badge: '/favicon.png',
        //custom data used when handling the event in the notification (click or others)
        data: {
            tag: payload.tag, // allows us to identify notification
            clickActionUrl: payload.clickUrl ? payload.clickUrl : self.location.origin + '/Notifications',
        },
        // notification action button
        actions: [
            {
                //action label
                action: 'close',
                // action title
                title: 'Close'
            },
            {
                //action label
                action: 'explore',
                // action title
                title: 'Go to the site'
            }
        ],
        // tag used to replace any simmilar notif
        tag: payload.tag ? payload.tag : null
    }

    // wait until notification displayed
    event.waitUntil(self.registration.showNotification(title, options))
});

//listen to any notification click
self.addEventListener('notificationclick', function (event) {
    var notification = event.notification
    var action = event.action

    //handle if action is close 
    if (action === 'close') {
        notification.close()
    }
    else {
        event.waitUntil(
            self.clients.matchAll({ type: 'window', includeUncontrolled: true }).then(function (clientList) {
                // URL you want to open
                const urlToOpen = new URL(event.notification.data.clickActionUrl, self.location.origin).href;

                // Check if there's a tab open with the same origin and path
                for (var i = 0; i < clientList.length; i++) {
                    var client = clientList[i];
                    if (client.url === urlToOpen && 'focus' in client) {
                        notification.close();
                        return client.focus(); // Focus the existing tab
                    }
                }

                // If no matching tab found, open a new tab
                if (self.clients.openWindow) {
                    notification.close();
                    return self.clients.openWindow(urlToOpen);
                }
            })
        );
    }
});
