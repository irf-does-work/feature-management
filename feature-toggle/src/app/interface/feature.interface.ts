import { FormControl } from "@angular/forms";
import { FeatureStatus , FeatureType} from "../enum/feature.enum";
import { UrlMatchResult } from "@angular/router";

export interface ILoginForm {
    email: FormControl<string | null>;
    password: FormControl<string | null>;
}

export interface ILoginAccept {
    email: string | null;
    password: string | null;
}

export interface ISignUpForm {
    fullName: FormControl<string | null>;
    email: FormControl<string | null>;
    password : FormControl<string | null>;
    confirmPassword : FormControl<string | null>;
}

export interface ISignUpAccept {
    name : string | null;
    email: string | null;
    password: string | null;
}

export interface IFeature {
    FeatureId: number;
    name: string;
    type: FeatureType;
    status: FeatureStatus;
}

export interface IRetrievedFeatures{
    featureFlagId: number;
    featureId: number;
    featureName: string;
    featureType: number;
    isEnabled: boolean | null;
}

export interface IselectedFilters{
    featureFilter: boolean | null;
    releaseFilter: boolean | null;
    enabledFilter: boolean | null;
    disabledFilter: boolean | null;
}

export interface IBusiness {
    name: string;
    businessId: string;
}

export interface IUpdateToggle{
    UserId : string | undefined;
    featureId: number;
    businessId : number | null;
    enableOrDisable: boolean;
}

