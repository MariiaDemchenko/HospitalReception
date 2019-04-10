const requestAllPhotosType = 'REQUEST_All_PHOTOS';
const receiveAllPhotosType = 'RECEIVE_ALL_PHOTOS';
const selectPhotoType = 'SELECT_PHOTO';
const editPhotoType = 'EDIT_PHOTO_u';
const viewPhotoType = 'VIEW_PHOTO_u';
const beforeRedirectType = 'BEFORE_REDIRECT_P';
const initialState = { photos: [], isLoading: false, redirectToView: false, redirectToEdit: false, hasMore: true, pageIndex: 0, selectedIndex: "", photoId: "" };
const pageSize = 9;

export const actionCreators = {
    requestAllPhotos: pageIndex => async (dispatch, getState) => {

        dispatch({ type: requestAllPhotosType, pageIndex });
        const url = `api/Photos/GetAllPhotos?pageIndex=${pageIndex}&pageSize=${pageSize}`;
        const response = await fetch(url);
        const photos = await response.json();
        dispatch({ type: receiveAllPhotosType, pageIndex, photos });
    },

    onSelectImage: (index) => ({ type: selectPhotoType, index }),
    onPhotoEditClick: () => ({ type: editPhotoType }),
    onPhotoViewClick: () => ({ type: viewPhotoType }),
    onBeforeRedirect: () => ({ type: beforeRedirectType })
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestAllPhotosType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveAllPhotosType) {
        return {
            ...state,
            pageIndex: action.photos.length > 0 ? state.pageIndex + 1 : state.pageIndex,
            hasMore: action.photos.length === pageSize,
            photos: [...state.photos, ...action.photos],
            isLoading: false
        };
    }

    if (action.type === selectPhotoType) {
        return {
            ...state,
            photos: state.photos.map((photo, i) => ({
                ...photo,
                isSelected: i !== action.index ? photo.isSelected : (photo.hasOwnProperty("isSelected") ? !photo.isSelected : true)
            }))
        }
    }


    if (action.type === editPhotoType) {

        return {
            ...state,
            redirectToEdit: true
        }
    }

    if (action.type === viewPhotoType) {
        return {
            ...state,
            redirectToView: true
        }
    }

    if (action.type === beforeRedirectType) {
        return {
            ...state,
            redirectToView: false,
            redirectToEdit: false
        }
    }

    return state;
}