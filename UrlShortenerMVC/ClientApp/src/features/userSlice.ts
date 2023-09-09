import {createSlice} from '@reduxjs/toolkit';
import storage from 'react-secure-storage';


const initialState = {
    user: await getUser()
}

