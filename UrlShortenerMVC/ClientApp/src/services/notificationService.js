import {Store} from 'react-notifications-component'
import 'react-notifications-component/dist/theme.css'

export default class Notifications{
    success(message: string, title: string = "Success"){
        Store.addNotification({
            title,
            message,
            type: "success",
            insert: "top",
            container: "top-right",
            animationIn: ["animate__animated", "animate__fadeIn"],
            animationOut: ["animate__animated", "animate__fadeOut"],
            dismiss: {
                duration: 5000,
                onScreen: true
            }
        })
    };
    error(message: string, title: string = "Error"){
        Store.addNotification({
            title,
            message,
            type: "danger",
            insert: "top",
            container: "top-right",
            animationIn: ["animate__animated", "animate__fadeIn"],
            animationOut: ["animate__animated", "animate__fadeOut"],
            dismiss: {
                duration: 5000,
                onScreen: true
            }
        })
    };
    warning(message: string, title: string = "Warning"){
        Store.addNotification({
            title,
            message,
            type: "warning",
            insert: "top",
            container: "top-right",
            animationIn: ["animate__animated", "animate__fadeIn"],
            animationOut: ["animate__animated", "animate__fadeOut"],
            dismiss: {
                duration: 5000,
                onScreen: true
            }
        })
    };
}