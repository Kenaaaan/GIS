export interface Skola {
  _id: string;
  type: string;
  properties: SkolaProperties;
  geometry: Geometry;
  id: string;
}

export interface SkolaProperties {
  "@id": string;
  amenity: string;
  barrier?: string;
  "name:hr"?: string;
  "name:sr"?: string;
  "addr:city"?: string;
  "addr:housenumber"?: string;
  "addr:street"?: string;
  "contact:email"?: string;
  "contact:phone"?: string;
  "contact:website"?: string;
  building?: string;
  source?: string;
  fence_type?: string;
  "addr:postcode"?: string;
  old_name?: string;
  wheelchair?: string;
  "survey:date"?: string;
  "building:levels"?: string;
  wikidata?: string;
  "isced:level"?: string;
  grades?: string;
  religion?: string;
  phone?: string;
  wikimedia_commons?: string;
  layer?: string;
  check_date?: string;
  name: string;
  "@geometry": string;
  "addr:country"?: string;
  "contact:fax"?: string;
  wikipedia?: string;
  denomination?: string;
  operator?: string;
  opening_hours?: string;
  start_date?: string;
  "name:fr"?: string;
  school?: string;
  "operator:type"?: string;
}

export interface Geometry {
  type: string;
  coordinates: number[];
}
