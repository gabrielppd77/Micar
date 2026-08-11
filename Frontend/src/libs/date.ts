import dayjs from "dayjs";
import customParseFormat from "dayjs/plugin/customParseFormat";
import { z } from "zod";

dayjs.extend(customParseFormat);

const DISPLAY_FORMAT = "DD/MM/YYYY";
const API_FORMAT = "YYYY-MM-DD";
const APP_TIME_ZONE = "America/Sao_Paulo";

function parseDateInput(value: string): string | null {
  const parsed = dayjs(value, DISPLAY_FORMAT, true);
  return parsed.isValid() ? parsed.format(API_FORMAT) : null;
}

export function toDateInput(isoDate: string) {
  return dayjs(isoDate, API_FORMAT).format(DISPLAY_FORMAT);
}

export function todayDateInput() {
  const parts = new Intl.DateTimeFormat("en-CA", {
    timeZone: APP_TIME_ZONE,
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
  }).formatToParts(new Date());

  const year = parts.find((part) => part.type === "year")?.value;
  const month = parts.find((part) => part.type === "month")?.value;
  const day = parts.find((part) => part.type === "day")?.value;

  return `${day}/${month}/${year}`;
}

function buildOptionalDateSchema() {
  return z
    .string()
    .trim()
    .transform((value, ctx) => {
      if (!value) return undefined;
      const parsed = parseDateInput(value);
      if (!parsed) {
        ctx.addIssue({
          code: z.ZodIssueCode.custom,
          message: "Informe uma data válida.",
        });
        return z.NEVER;
      }
      return parsed;
    });
}

function buildRequiredDateSchema() {
  return buildOptionalDateSchema().refine(
    (value): value is string => value !== undefined,
    "Informe a data.",
  );
}

export function zDateBR(options: {
  required: false;
}): ReturnType<typeof buildOptionalDateSchema>;

export function zDateBR(options?: {
  required?: true;
}): ReturnType<typeof buildRequiredDateSchema>;
export function zDateBR(options: { required?: boolean } = {}) {
  return options.required === false
    ? buildOptionalDateSchema()
    : buildRequiredDateSchema();
}
