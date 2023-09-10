import storage from 'react-secure-storage'
import {JWT_STORAGE_KEY} from '../constants'

export const getJWT = () => {
    return storage.getItem(JWT_STORAGE_KEY)
}