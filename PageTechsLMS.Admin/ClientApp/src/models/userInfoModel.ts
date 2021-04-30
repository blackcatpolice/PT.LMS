

import { useState, useCallback } from 'react'
import { Client } from '../apis/API';
import { CurrentUser } from '@/interfaces/userInfo/userInfo'

export default function userInfoModel() {
    const [userInfo, setUser] = useState<CurrentUser | null>()

    const client = new Client();

    const getUserInfo = useCallback(async () => {
        const res = await client.getUserInfo()
        console.log(res)
        setUser({
            avatar: res?.data && res?.data['avatar'] || '@/assets/avatar.png',
            name: res?.data['name']
        })

        console.log(userInfo)
    }, []);

    const setInfo = useCallback((user) => {
        setUser(user)
    }, [])

    return {
        userInfo, getUserInfo
    }
}