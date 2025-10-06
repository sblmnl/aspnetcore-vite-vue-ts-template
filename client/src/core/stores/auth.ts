import type { GetAuthenticatedUserApiResponse } from "@/core/models/api/auth/me";
import { getAppRoute } from "@/router";
import axios from "axios";
import { DateTime } from "luxon";
import { defineStore } from "pinia";
import { reactive, toRefs } from "vue";

const REFRESH_INTERVAL_MS = 30 * 1000;

export type AuthStoreData = {
  user: GetAuthenticatedUserApiResponse | null;
};

export const useAuthStore = defineStore(
  "auth",
  () => {
    const state = reactive<AuthStoreData>({
      user: null,
    });

    const lastRefresh: DateTime | null = null;

    async function refresh(manualRefresh: boolean = true) {
      if (
        !manualRefresh &&
        lastRefresh != null &&
        DateTime.now() <= lastRefresh.plus({ milliseconds: REFRESH_INTERVAL_MS })
      ) {
        return;
      }

      const res = await axios.get(getAppRoute("/api/me"));

      state.user = res.data;
    }

    async function setupWithAutoRefresh() {
      await refresh(false);

      setInterval(async () => {
        await refresh(false);
      }, REFRESH_INTERVAL_MS);
    }

    return { ...toRefs(state), lastRefresh, refresh, setupWithAutoRefresh };
  },
  { persist: { storage: sessionStorage } },
);
