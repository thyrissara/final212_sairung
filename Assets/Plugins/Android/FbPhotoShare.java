package com.final212.sharefacebookphoto;

import android.net.Uri;

import com.facebook.share.model.ShareLinkContent;

public class FbPhotoShare {
    public static void Test()
    {
        ShareLinkContent content = new ShareLinkContent.Builder()
                .setContentUrl(Uri.parse("https://developers.facebook.com"))
                .build();
    }
}
