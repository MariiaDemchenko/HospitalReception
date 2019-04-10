const requestAlbumPhotosType = 'REQUEST_ALBUM_PHOTOS';
const receiveAlbumPhotosType = 'RECEIVE_ALBUM_PHOTOS';

const requestAllPhotosType = 'REQUEST_ALBUM_PHOTOS_TO_EDIT';
const receiveAllPhotosType = 'RECEIVE_ALBUM_PHOTOS_TO_EDIT';

const viewPhotoType = 'VIEW_PHOTOS';
const addPhotoType = 'ADD_PHOTOS';
const editPhotoType = 'EDIT_PHOTOS_IN_ALBUM';
const removePhotoType = 'REMOVE_PHOTOS_IN_ALBUM';

const selectPhotoType = 'SELECT_PHOTO';
const beforeRedirectType = 'BEFORE_REDIRECT_TO_PHOTO';

const initAlbumType = 'ON_INIT_ALBUM_PHOTOS'
const albumNameChangedType = 'ON_EDIT_ALBUM_NAME'
const albumDescriptionChangedType = 'ON_EDIT_ALBUM_DESCRIPTION'

const saveChangesType = 'ALBUM_SAVE_CHANGES'

const updateSuccessType = 'ALBUM_UPDATE_SUCCESS'
const updateErrorType = 'ALBUM_UPDATE_ERROR'

const removeSuccessType = 'ALBUM_PHOTOS_REMOVE_SUCCESS'
const removeErrorType = 'ALBUM_PHOTOS_REMOVE_ERROR'

const nameOnBlurType = 'ALBUM_NAME_BLUR'
const descriptionOnBlurType = 'ALBUM_DESCRIPTION_BLUR'

const pageSize = 9;

const initialState = {
    albumPhotos: [],
    formErrors: { name: '', description: '' },
    selectedAlbum: { albumName: "", description: "" },
    isLoading: false,
    selectedIndex: null,
    redirect: false,
    hasMore: false,
    pageIndex: 0,
    redirectToEdit: false,
    redirectToView: false,
    postSaveChanges: false,
    redirectToAlbums: false,
    redirectToRemove: false,
    alertMessage: null,
    alertId: 0,
    alertType: "",
    currentDate: "",
    refresh: false,
    albumName: "",
    description: "",
    validateName: false,
    validateDescription: false
};

export const actionCreators = {
    requestAlbumPhotos: (pageIndex, selectedIndex) => async (dispatch) => {
        const url = `api/Photos/GetPhotosByAlbumId?albumId=${selectedIndex}&pageIndex=${pageIndex}&pageSize=${pageSize}`;
        dispatch({ type: requestAlbumPhotosType, pageIndex });
        const response = await fetch(url);
        const albumPhotos = await response.json();
        dispatch({ type: receiveAlbumPhotosType, pageIndex, albumPhotos });
    },

    requestAllPhotos: (pageIndex, selectedIndex) => async (dispatch) => {

        const url = `api/Photos/GetAllPhotos?pageIndex=${pageIndex}&pageSize=${pageSize}`;
        dispatch({ type: requestAllPhotosType, pageIndex });
        const response = await fetch(url);
        const albumPhotos = await response.json();
        dispatch({ type: receiveAllPhotosType, pageIndex, albumPhotos, selectedIndex });
    },

    onPhotoViewClick: () => ({ type: viewPhotoType }),
    onPhotoEditClick: () => ({ type: editPhotoType }),
    onPhotoAddClick: (index) => ({ type: addPhotoType, index }),
    onPhotoRemoveClick: (index) => ({ type: removePhotoType, index }),

    onSelectImage: (index) => ({ type: selectPhotoType, index }),

    onInitAlbum: (selectedAlbum) => ({ type: initAlbumType, selectedAlbum }),
    onAlbumNameChange: (event) => ({ type: albumNameChangedType, event }),
    onAlbumDescriptionChange: (event) => ({ type: albumDescriptionChangedType, event }),

    onSaveChangesClick: () => ({ type: saveChangesType }),
    onRequestSuccess: () => ({ type: updateSuccessType }),
    onRequestError: (message) => ({ type: updateErrorType, message }),

    onRemoveSuccess: () => ({ type: removeSuccessType }),
    onRemoveError: (message) => ({ type: removeErrorType, message }),

    onBeforeRedirect: () => ({ type: beforeRedirectType }),

    nameOnBlur: () => ({ type: nameOnBlurType }),
    descriptionOnBlur: () => ({ type: descriptionOnBlurType })
};

export const reducer = (state, action) => {

    state = state || initialState;

    if (action.type === requestAlbumPhotosType) {
        return {
            ...state,
            isLoading: true,
            refresh: false
        };
    }

    if (action.type === receiveAlbumPhotosType) {
        return {
            ...state,
            pageIndex: action.albumPhotos.length > 0 ? state.pageIndex + 1 : state.pageIndex,
            hasMore: action.albumPhotos.length === pageSize,
            albumPhotos: [...state.albumPhotos || [], ...action.albumPhotos],
            isLoading: false,
            refresh: false
        };
    }

    if (action.type === requestAllPhotosType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveAllPhotosType) {
        return {
            ...state,

            pageIndex: action.albumPhotos.length > 0 ? state.pageIndex + 1 : state.pageIndex,
            hasMore: action.albumPhotos.length === pageSize,
            albumPhotos: [...state.albumPhotos || [], ...action.albumPhotos.map((albumPhoto, i) => ({
                ...albumPhoto,
                isSelected: state.selectedAlbum.photos.includes(albumPhoto.photoId) ? true : false
            }))],
            isLoading: false
        };
    }

    if (action.type === viewPhotoType) {
        if (state.albumPhotos.filter(albumPhoto => albumPhoto.isSelected).length === 0) {
            return {
                ...state,
                alertMessage: "Please select photos to view",
                alertId: state.alertId + 1,
                alertType: "info"
            }
        }
        return {
            ...state,
            redirectToView: true
        }
    }

    if (action.type === editPhotoType) {
        if (state.albumPhotos.filter(albumPhoto => albumPhoto.isSelected).length === 0) {
            return {
                ...state,
                alertMessage: "Please select photos to edit",
                alertId: state.alertId + 1,
                alertType: "info"
            }
        }
        return {
            ...state,
            redirectToEdit: true
        }
    }

    if (action.type === addPhotoType) {
        return {
            ...state,
            selectedIndex: action.index,
            redirect: true
        }
    }

    if (action.type === removePhotoType) {
        if (state.albumPhotos.filter(albumPhoto => albumPhoto.isSelected).length === 0) {
            return {
                ...state,
                alertMessage: "Please select photos to remove",
                alertId: state.alertId + 1,
                alertType: "info"
            }
        }
        return {
            ...state,
            redirectToRemove: true
        }
    }

    if (action.type === saveChangesType) {
        const selectedAlbumName = state.selectedAlbum.albumName || ""
        const selectedAlbumDescription = state.selectedAlbum.description || ""
        const formErrors = {
            ...state.formErrors,
            name: selectedAlbumName.length > 0 ? "" : "Name should not be empty",
            description: selectedAlbumDescription.length > 0 ? "" : "Description should not be empty"
        }
        if (formErrors.name.length > 0 || formErrors.description.length > 0) {
            return {
                ...state,
                alertMessage: "The album data is invalid",
                alertId: state.alertId + 1,
                formErrors: formErrors,
                alertType: "warning",
                postSaveChanges: false,

            }
        }

        return {
            ...state,
            postSaveChanges: true
        };
    }


    if (action.type === selectPhotoType) {
        const photoId = state.albumPhotos[action.index].photoId
        const selected = state.albumPhotos[action.index].isSelected

        if (!state.selectedAlbum) {
            state.selectedAlbum = {}
        }


        if (!state.selectedAlbum.photos) {
            state.selectedAlbum.photos = []
        }

        if (!selected)
            state.selectedAlbum.photos.push(photoId)

        return {
            ...state,
            albumPhotos: state.albumPhotos.map((photo, i) => ({
                ...photo,
                isSelected: i !== action.index ? photo.isSelected : (photo.hasOwnProperty("isSelected") ? !photo.isSelected : true)
            })),
            selectedAlbum: { ...state.selectedAlbum, photos: selected ? state.selectedAlbum.photos.filter(photo => photo !== photoId) : state.selectedAlbum.photos }
        }
    }

    if (action.type === albumNameChangedType) {
        return {
            ...state,
            selectedAlbum: { ...state.selectedAlbum, albumName: action.event.target.value },
            formErrors: { ...state.formErrors, name: state.validateName ? (action.event.target.value.length > 0 ? "" : "Name should not be empty") : "" }
        };
    }

    if (action.type === albumDescriptionChangedType) {
        return {
            ...state,
            selectedAlbum: { ...state.selectedAlbum, description: action.event.target.value },
            formErrors: { ...state.formErrors, description: state.validateDescription ? (action.event.target.value.length > 0 ? "" : "Description should not be empty") : "" }
        };
    }

    if (action.type === initAlbumType) {
        return {
            ...state,
            selectedAlbum: action.selectedAlbum || state.selectedAlbum,
            pageIndex: 0,
            currentDate: new Date().getTime()
        };
    }

    if (action.type === beforeRedirectType) {
        return {
            ...state,
            redirect: false,
            redirectToEdit: false,
            redirectToView: false,
            redirectToAlbums: false,
            redirectToRemove: false,
            pageIndex: 0,
            alertMessage: null,
            selectedAlbum: { albumName: "", description: "" },
            alertId: 0,
            hasMore: false,
            albumPhotos: [],
            formErrors: { name: '', description: '' }
        }
    }

    if (action.type === updateSuccessType) {
        return {
            postSaveChanges: false,
            redirectToAlbums: true,
            alertMessage: "Please select one album to view",
            alertId: state.alertId + 1,
            alertType: "success"
        }
    }

    if (action.type === removeSuccessType) {
        return {
            pageIndex: 0,
            postSaveChanges: false,
            alertMessage: "Photos have been removed successfully",
            alertId: state.alertId + 1,
            alertType: "success",
            redirectToRemove: false,
            refresh: true
        }
    }

    if (action.type === updateErrorType) {
        return {
            ...state,
            postSaveChanges: false,
            alertMessage: action.message,
            alertId: action.message === state.alertMessage ? state.alertId : state.alertId + 1,
            alertType: "danger"
        }
    }

    if (action.type === removeErrorType) {
        return {
            ...state,
            postSaveChanges: false,
            alertMessage: action.message,
            alertId: action.message === state.alertMessage ? state.alertId : state.alertId + 1,
            alertType: "warning"
        }
    }

    if (action.type === nameOnBlurType) {
        const albumName = state.selectedAlbum.albumName || ""
        return {
            ...state,
            validateName: true,
            formErrors: { ...state.formErrors, name: albumName.length > 0 ? "" : "Name should not be empty" }
        }
    }

    if (action.type === descriptionOnBlurType) {
        const description = state.selectedAlbum.description || ""
        return {
            ...state,
            validateDescription: true,
            formErrors: { ...state.formErrors, description: description.length > 0 ? "" : "Description should not be empty" }
        }
    }

    return state;
}