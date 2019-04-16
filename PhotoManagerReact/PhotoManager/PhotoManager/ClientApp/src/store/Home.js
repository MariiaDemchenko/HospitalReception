const requestImagesType = 'REQUEST_DEMO_PHOTOS';
const receiveImagesType = 'RECEIVE_DEMO_PHOTOS';

const createAlertType = 'CREATE_ALERT';

const initialState = {
    images: [],

    isLoading: false,

    alertMessage: null,
    alertId: 0,
    alertType: ""
};

export const actionCreators = {

    requestDemoPhotos: p=> (dispatch) => {
        dispatch({ type: requestImagesType });
        fetch(`api/Home/GetDemoPhotos`)
            .then(function (response) {
                if (response.ok) {
                    response.json().then(images => {
                        dispatch({ type: receiveImagesType, images })
                    })
                }
                else {
                    dispatch({ type: createAlertType })
                    dispatch({ type: receiveImagesType, images: [] })
                }
            })
    }
};

export const reducer = (state, action) => {
    state = state || initialState;


    if (action.type === requestImagesType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveImagesType) {
        return {
            ...state,
            images: [...state.images || [], ...action.images],
            isLoading: false
        };
    }
   
    if (action.type === createAlertType) {
        return {
            ...state,
            alertMessage: "Error getting albums",
            alertId: state.alertId + 1,
            alertType: "danger"
        }
    }

    return state;
}