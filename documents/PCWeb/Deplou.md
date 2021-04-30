

    
    dotnet PagetechsLMS.WebAndApi.dll  --urls="http://*:5002"

    
    yum install autoconf automake libtool
    yum install freetype-devel fontconfig libXft-devel
    yum install libjpeg-turbo-devel libpng-devel giflib-devel libtiff-devel libexif-devel
    yum install glib2-devel cairo-devel
    git clone https://github.com/mono/libgdiplus
    cd libgdiplus
    ./autogen.sh
    make
    make install
    cd /usr/lib64/
    ln -s /usr/local/lib/libgdiplus.so gdiplus.dll