function showNotification(text, type, duration)
{
    console.log("notifying", { text, type, duration });
    bulmaToast.toast({ message: text, type: type, duration: duration });
}

bulmaToast.setDefaults({
    duration: 2000,
    position: 'bottom-right',
    closeOnClick: true,
    animate: { in: 'bounceInRight', out: 'bounceOutRight' }
});