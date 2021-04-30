import { useState, useCallback, useEffect } from 'react'
import { useModel } from 'umi';
import axios from 'axios';
import AuthService from '@/services/AuthService';
import IdentityService from '@/services/IdentityService';
import { Client } from '../apis/API'

import { WebStorageStateStore } from 'oidc-client';
import CountDown from 'ant-design-pro/lib/CountDown';
import { ExceptionOutlined } from '@ant-design/icons';

interface userModel {
    token: string
}

export default function userAuthModel() {
    const [user, setToken] = useState<userModel | null>(null)
    const [role, setRole] = useState<any>(null);
    const [permission, setPermission] = useState<any>(null);
    const identityClient = new Client();
    const { userInfo, getUserInfo } = useModel("userInfoModel")

    const authService = new AuthService();
    const identityService = new IdentityService();

    useEffect(() => {
        const timer = setInterval(() => {
            authService.refreshToken(window.localStorage.getItem('refreshToken') || '').then(res => {
                setRefreshToken(res['data'])
            })
        }, 1700 * 1000);
        return () => clearInterval(timer);
    }, []);

    const signin = useCallback(async (account, password) => {

        var res = await authService.loginPwd(account, password);

        if (res['status'] == 200) {
            var resData = res['data'];
            try {
                setRefreshToken(resData)
            } catch (error) {
                console.log(error)
            }

            await getRoles();
            console.log('test')
            //await getPerissmion();
            await getUserInfo();

            console.log('test')
            setToken({
                token: resData.access_token
            })
            return true;
        } else
            return false;
    }, [])

    const setRefreshToken = (resData: any) => {
        window.localStorage['token'] = resData.access_token
        let expires_in = resData.expires_in
        window.localStorage['refreshToken'] = resData.refresh_token
    }

    const signout = useCallback(() => {
        // signout implementation
        setToken(null)
    }, [])

    const getRoles = useCallback(async () => {
        var res = await identityClient.getRole()
        setRole(res);
    }, []);

    const getPerissmion = useCallback(() => {
        // identityClient.getPermission().then(res => {
        //     setPermission(res?.data)
        // })
    }, []);

    return {
        user,
        role,
        permission,
        signin,
        signout
    }
}