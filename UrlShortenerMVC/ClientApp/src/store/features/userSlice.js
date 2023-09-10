import { createSlice } from "@reduxjs/toolkit";
import { User } from "../../types/Users";

interface userSliceState{
    user: User
}

const initialState: userSliceState = {
    user: new User()
}

const userSlice = createSlice({
    initialState,
    name: 'userSlice',
    reducers:{
        remove(state){
            state.user = new User();
        },
        addUser(state, action){
            state.user = action.payload;
        }
    },
});

export const {remove, addUser} = userSlice.actions;
export default userSlice.reducer;
